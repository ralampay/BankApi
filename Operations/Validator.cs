namespace BankApi.Operations;

public abstract class Validator
{
    public List<string> Messages { get; set; }
    public List<Dictionary<string, object>> MessageHashes { get; set; }

    public Validator()
    {
        Messages = new List<string>();
        MessageHashes = new List<Dictionary<string, object>>();
    }

    public abstract void run();

    protected void AddError(String msg, String key)
    {
        Messages.Add(msg);
        
        Dictionary<string, object> error = new Dictionary<string, object>();
        error[key] = msg;
        MessageHashes.Add(error);
    }

    public bool HasErrors {
        get {
            return Messages.Count > 0;
        }
    }

    public Dictionary<string, object> Payload {
        get {
            Dictionary<string, object> payload = new Dictionary<string, object>();

            payload["messages"]         = Messages;
            payload["messageHashes"]    = MessageHashes;

            return payload;
        }
    }
}