defmodule Advent2024.Day01A do
  def run(input) do
    # Your function logic here

    input
    |> Enum.map(fn x ->
      # [a, b] = String.split(x, " ", trim: true)
      List.to_tuple(x)
    end)
    # |> IO.inspect()
    |> Enum.unzip()
    |> Tuple.to_list()
    |> Enum.map(&Enum.sort/1)
    |> Enum.zip()
    # |> Enum.map(fn {a, b} -> abs(String.to_integer(a) - String.to_integer(b)) end)
    |> Enum.map(fn {a, b} -> abs(a - b) end)
    |> Enum.sum()
  end
end
