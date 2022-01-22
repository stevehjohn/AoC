//using JetBrains.Annotations;

//namespace AoC.Solutions.Solutions._2020._19;

//[UsedImplicitly]
//public class Part1 : Base
//{
//    private List<string> _messages = new();

//    public override string GetAnswer()
//    {
//        ParseInput();

//        return "TESTING";
//    }

//    private void ParseInput()
//    {
//        var i = 0;

//        var rules = new Dictionary<int, string>();

//        while (! string.IsNullOrWhiteSpace(Input[i]))
//        {
//            var split = Input[i].Split(':');

//            rules.Add(int.Parse(split[0]), split[1]);

//            i++;
//        }

//        i++;

//        while (i < Input.Length)
//        {
//            _messages.Add(Input[i]);

//            i++;
//        }
//    }
//}
