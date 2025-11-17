using System.ServiceModel;
using System.ServiceModel.Channels;

namespace ComercializadoraAPI.SoapClient
{
    public class BancoServiceClient : IDisposable
    {
        private readonly ChannelFactory<IBancoServiceClient> _channelFactory;
        private IBancoServiceClient? _client;

        public BancoServiceClient(string serviceUrl)
        {
            var binding = new BasicHttpBinding
            {
                MaxReceivedMessageSize = 2147483647,
                MaxBufferSize = 2147483647,
                OpenTimeout = TimeSpan.FromMinutes(1),
                CloseTimeout = TimeSpan.FromMinutes(1),
                SendTimeout = TimeSpan.FromMinutes(10),
                ReceiveTimeout = TimeSpan.FromMinutes(10)
            };

            var endpoint = new EndpointAddress(serviceUrl);
            _channelFactory = new ChannelFactory<IBancoServiceClient>(binding, endpoint);
        }

        private IBancoServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = _channelFactory.CreateChannel();
                }
                return _client;
            }
        }

        public async Task<EvaluateCreditResponse> EvaluateCreditAsync(EvaluateCreditRequest request)
        {
            return await Client.EvaluateCreditAsync(request);
        }

        public async Task<GetAmortizationScheduleResponse> GetAmortizationScheduleAsync(GetAmortizationScheduleRequest request)
        {
            return await Client.GetAmortizationScheduleAsync(request);
        }

        public async Task<GetClientInfoResponse> GetClientInfoAsync(GetClientInfoRequest request)
        {
            return await Client.GetClientInfoAsync(request);
        }

        public void Dispose()
        {
            if (_client is IClientChannel channel)
            {
                try
                {
                    if (channel.State != CommunicationState.Faulted)
                    {
                        channel.Close();
                    }
                    else
                    {
                        channel.Abort();
                    }
                }
                catch
                {
                    channel.Abort();
                }
            }

            _channelFactory?.Close();
        }
    }
}
