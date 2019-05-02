using ElectrumXClient.Request;
using ElectrumXClient.Response;
using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ElectrumXClient
{
    public class Client : IDisposable
    {
        private string _host;
        private int _port;
        private bool _useSSL;
        TcpClient _tcpClient;
        SslStream _sslStream;
        NetworkStream _tcpStream;
        Stream _stream;
        readonly int BUFFERSIZE = 32;

        public Client(string host, int port, bool useSSL)
        {
            _host = host;
            _port = port;
            _useSSL = useSSL;
        }

        private async Task Connect()
        {
            _tcpClient = new TcpClient();
            await _tcpClient.ConnectAsync(_host, _port);
            if (_useSSL)
            {
                _sslStream = new SslStream(_tcpClient.GetStream(), true,
                    new RemoteCertificateValidationCallback(CertificateValidationCallback));
                await _sslStream.AuthenticateAsClientAsync(_host);
                _stream = _sslStream;
            }
            else
            {
                _tcpStream = _tcpClient.GetStream();
                _stream = _tcpStream;
            }
        }

        private void Disconnect()
        {
            _stream.Close();
            _tcpClient.Close();
        }

        public async Task<VersionResponse> GetVersion()
        {
            var request = new VersionRequest();
            var requestData = request.GetRequestData<VersionRequest>();
            await this.Connect();
            string response = await SendMessage(requestData);
            this.Disconnect();
            return new VersionResponse(response);
        }

        private async Task<string> SendMessage(byte[] requestData)
        {
            var response = string.Empty;
            var buffer = new byte[BUFFERSIZE];
            await _stream.WriteAsync(requestData, 0, requestData.Length);

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = await _stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                    if (read < BUFFERSIZE) break;
                }
                response = System.Text.Encoding.ASCII.GetString(ms.ToArray());
            }

            return response;
        }

        private static bool CertificateValidationCallback(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // We don't check the validity of the certificate (yet)
            return true;
        }

        public void Dispose()
        {
            if (_sslStream != null) ((IDisposable)_sslStream).Dispose();
            ((IDisposable)_tcpClient).Dispose();
        }
    }
}
