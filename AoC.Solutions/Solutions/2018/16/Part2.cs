﻿using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._16;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly Cpu _cpu = new();

    private readonly Dictionary<string, HashSet<int>> _opCodes = new();

    private readonly Dictionary<int, string> _matchedOpCodes = new();

    public override string GetAnswer()
    {
        _cpu.Initialise();

        foreach (var opCode in OpCodes)
        {
            _opCodes.Add(opCode, new HashSet<int>());
        }

        var lastSampleLine = RunSamples();

        ExactMatchOpCodes();

        ExecuteTestProgram(lastSampleLine + 2);

        return _cpu.GetRegisters()[0].ToString();
    }

    private void ExecuteTestProgram(int startLine)
    {
        _cpu.SetRegisters(new[] { 0, 0, 0, 0 });

        for (var i = startLine; i < Input.Length; i++)
        {
            var line = Input[i];

            var parts = line.Split(' ').Select(int.Parse).ToArray();

            _cpu.Execute(_matchedOpCodes[parts[0]], parts[1], parts[2], parts[3]);
        }
    }

    private void ExactMatchOpCodes()
    {
        while (_opCodes.Count > 0)
        {
            var definite = _opCodes.First(c => c.Value.Count == 1);

            var opCodeNumber = definite.Value.Single();

            _matchedOpCodes.Add(opCodeNumber, definite.Key);

            _opCodes.Remove(definite.Key);

            foreach (var (_, possibilities) in _opCodes)
            {
                possibilities.Remove(opCodeNumber);
            }
        }
    }

    private int RunSamples()
    {
        int i;

        for (i = 0; i < Input.Length; i += 4)
        {
            if (string.IsNullOrWhiteSpace(Input[i]))
            {
                break;
            }

            var parts = Input[i + 1].Split(' ').Select(int.Parse).ToArray();

            ExecuteSample(parts[0], parts[1], parts[2], parts[3], ParseRegisters(Input[i]), ParseRegisters(Input[i + 2]));
        }

        return i;
    }

    private void ExecuteSample(int code, int a, int b, int c, int[] initial, int[] expected)
    {
        for (var i = 0; i < OpCodes.Length; i++)
        {
            _cpu.SetRegisters(initial);

            _cpu.Execute(OpCodes[i], a, b, c);

            if (expected.SequenceEqual(_cpu.GetRegisters()))
            {
                _opCodes[OpCodes[i]].Add(code);
            }
        }
    }
}