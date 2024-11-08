using System.ComponentModel;

namespace OrderBookAPI.Models
{
    public enum PickupPlace
    {
        [Description("Butik (Store)")]
        Butik = 0,

        [Description("Hjemme (Home)")]
        Hjemme = 1,

        [Description("Post (Post Office)")]
        Post = 2
    }
}
