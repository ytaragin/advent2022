defmodule Advent2024.Day03B do
  def get_mul_fields(line) do
    Regex.scan(~r/mul\((\d+),(\d+)\)/, line)
    |> Enum.map(fn [_, a, b] -> {String.to_integer(a), String.to_integer(b)} end)
  end

  # |> String.split(~r/do\(\)|don't\(\)/)
  def split_line(line) do
    line
    |> String.split("do()")
    |> Enum.flat_map(fn part ->
      part
      |> String.split("don't()")
      |> List.first()
      |> List.wrap()
    end)

    # |> IO.inspect()
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
    |> Enum.join()
    # |> Enum.map(&get_mul_sum/1)
    # |> Enum.flat_map(&split_line/1)
    |> split_line()
    # |> IO.inspect()
    |> Enum.map(&get_mul_sum/1)
    |> Enum.sum()
  end
end
