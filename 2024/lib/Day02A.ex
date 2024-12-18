defmodule Advent2024.Day02A do
  def change_list(list) do
    Enum.chunk_every(list, 2, 1, :discard)
    |> Enum.map(fn [a, b] -> a - b end)

    # |> IO.inspect()
  end

  def check_change_list?(list) do
    sign_setter = List.first(list)

    Enum.all?(list, fn diff ->
      abs(diff) >= 1 and abs(diff) <= 3 and same_sign?(diff, sign_setter)
    end)
  end

  def is_safe?(measures) do
    list1 = change_list(measures)

    check_change_list?(list1)
  end

  def same_sign?(a, b) do
    (a >= 0 and b >= 0) or (a < 0 and b < 0)
  end

  def run(input) do
    # Your function logic here

    input
    # |> Enum.map(fn x ->
    #   String.split(x, " ", trim: true)
    # end)
    # |> Enum.map(fn l -> Enum.map(l, &String.to_integer/1) end)
    |> Enum.map(&is_safe?/1)
    |> Enum.count(& &1)
  end
end
