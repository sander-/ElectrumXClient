using ElectrumXClient.Request;
using ElectrumXClient.Response;
using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

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

        public Client(string host, int port, bool useSSL)
        {
            _host = host;
            _port = port;
            _useSSL = useSSL;
        }

        private void Connect()
        {
            _tcpClient = new TcpClient(_host, _port);
            if (_useSSL)
            {
                _sslStream = new SslStream(_tcpClient.GetStream(), true,
                    new RemoteCertificateValidationCallback(CertificateValidationCallback));
                _sslStream.AuthenticateAsClient(_host);
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

        public VersionResponse GetVersion()
        {
            var request = new VersionRequest();
            var requestData = request.GetRequestData<VersionRequest>();
            this.Connect();
            string response = SendMessage(requestData);
            this.Disconnect();
            return new VersionResponse(response);
        }

        private string SendMessage(byte[] requestData)
        {
            _stream.Write(requestData, 0, requestData.Length);
            var responseData = new byte[256];
            string response = string.Empty;
            int bytes = _stream.Read(responseData, 0, responseData.Length);
            response = System.Text.Encoding.ASCII.GetString(responseData, 0, bytes);
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
