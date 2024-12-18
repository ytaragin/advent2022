defmodule Advent2024.Day01B do
  def getcount(list2, item) do
    Enum.count(list2, fn x -> x == item end)
  end

  def run(input) do
    # Your function logic here

    {list1, list2} =
      input
      |> Enum.map(fn x ->
        # [a, b] = String.split(x, " ", trim: true)
        # {a, b}
        List.to_tuple(x)
      end)
      # |> IO.inspect()
      |> Enum.unzip()

    # |> Tuple.to_list()

    # list1 = Enum.map(list1, &String.to_integer/1)
    # list2 = Enum.map(list2, &String.to_integer/1)

    # IO.inspect(list2)

    list1
    # |> IO.inspect()
    |> Enum.map(fn x ->
      x * Enum.count(list2, fn y -> y == x end)
    end)
    # |> IO.inspect()
    |> Enum.sum()
  end
end
