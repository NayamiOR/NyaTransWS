namespace NyaTransWS;

public class WsBase
{
    public delegate void UpdateStatusDelegate(Status status);

    public UpdateStatusDelegate? UpdateStatusDel { set; get; }
    public Status Status { get; set; }
}