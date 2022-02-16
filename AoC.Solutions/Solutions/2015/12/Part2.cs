using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AoC.Solutions.Solutions._2015._12;

[UsedImplicitly]
public class Part2 : Base
{
    private const string Exclude = "red";

    public override string GetAnswer()
    {
        dynamic json = JsonConvert.DeserializeObject(Input[0]);

        return GetSum(json).ToString();
    }

    private long GetSum(JObject jObject)
    {
        if (jObject.Properties().Select(a => a.Value).OfType<JValue>().Select(v => v.Value).Contains(Exclude))
        {
            return 0;
        }

        return jObject.Properties().Sum((dynamic a) => (long) GetSum(a.Value));
    }

    private long GetSum(JArray jArray)
    {
        return jArray.Sum((dynamic a) => (long) GetSum(a));
    }

    private static long GetSum(JValue value)
    {
        return value.Type == JTokenType.Integer ? (long) (value.Value ?? 0) : 0;
    }
}