#r "Microsoft.WindowsAzure.Storage"
#r "Newtonsoft.Json"

using System.Net;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    // 接続文字列
    var connectionString = ConfigurationManager.AppSettings["QUEUE_STORAGE_CONNECTION_STRING"];

    // ストレージアカウントを作成
    var account = CloudStorageAccount.Parse(connectionString);

    // Queueクライアントを作成
    var client = account.CreateCloudQueueClient();

    // キュー"order"の参照を取得
    var queue = client.GetQueueReference("order");

    // キューから先頭のmessageを取得
    var message = await queue.PeekMessageAsync();
    
    return req.CreateResponse(HttpStatusCode.OK, message);
}
