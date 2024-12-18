defmodule Advent2024.Main do
  def run do
    parts = [
      {Advent2024.Day01A, "inputs/day01_small.txt", true},
      {Advent2024.Day01A, "inputs/day01.txt", true},
      {Advent2024.Day01B, "inputs/day01_small.txt", true},
      {Advent2024.Day01B, "inputs/day01.txt", true},
      {Advent2024.Day02A, "inputs/day02_small.txt", true},
      {Advent2024.Day02A, "inputs/day02.txt", true},
      {Advent2024.Day02B, "inputs/day02_small.txt", true},
      {Advent2024.Day02B, "inputs/day02.txt", true},
      {Advent2024.Day03A, "inputs/day03.txt", false},
      {Advent2024.Day03B, "inputs/day03_small.txt", false},
      {Advent2024.Day03B, "inputs/day03.txt", false},
      {Advent2024.Day04A, "inputs/day04_small.txt", false},
      {Advent2024.Day04A, "inputs/day04.txt", false},
      {Advent2024.Day04B, "inputs/day04_small.txt", false},
      {Advent2024.Day04B, "inputs/day04.txt", false},
      {Advent2024.Day05A, "inputs/day05_small.txt", false},
      {Advent2024.Day05A, "inputs/day05.txt", false},
      {Advent2024.Day05B, "inputs/day05_small.txt", false},
      {Advent2024.Day05B, "inputs/day05.txt", false}
      # Add all parts here
    ]

    Enum.each(parts, fn {module, file, num_parse} ->
      result =
        File.read!(file)
        |> to_string()
        |> String.split("\n", trim: num_parse)
        |> (fn result ->
              if num_parse do
                result
                |> Enum.map(fn x ->
                  String.split(x, " ", trim: true)
                  |> Enum.map(&String.to_integer/1)
                end)
              else
                result
              end
            end).()
        |> module.run()

      IO.puts("Result for #{module}-#{file}: #{inspect(result)}")
    end)
  end
end
