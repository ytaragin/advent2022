defmodule Advent2024.Day03A do
  def get_mul_fields(line) do
    Regex.scan(~r/mul\((\d+),(\d+)\)/, line)
    |> Enum.map(fn [_, a, b] -> {String.to_integer(a), String.to_integer(b)} end)
  end

  def get_mul_sum(line) do
    line
    |> get_mul_fields()
    |> Enum.map(fn {a, b} -> a * b end)
    |> Enum.sum()
  end

  def run(input) do
    input
    # |> IO.inspect()
    |> Enum.map(&get_mul_sum/1)
    |> Enum.sum()
  end
end
