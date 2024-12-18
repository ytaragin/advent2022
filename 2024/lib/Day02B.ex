defmodule Advent2024.Day02B do
  # def change_list(list) do
  #   Enum.chunk_every(list, 2, 1, :discard)
  #   |> Enum.map(fn [a, b] -> a - b end)

  #   # |> IO.inspect()
  # end

  # def check_change_list?(list, sign_setter) do
  #   Enum.all?(list, fn diff ->
  #     abs(diff) >= 1 and abs(diff) <= 3 and same_sign?(diff, sign_setter)
  #   end)
  # end

  # def is_dampened_safex?(change_list, prefix, adder) do
  #   IO.inspect(change_list)
  #   IO.inspect(prefix)
  #   IO.inspect(adder)

  #   case change_list do
  #     [] ->
  #       false

  #     [head | tail] ->
  #       new_number = head + adder
  #       new_list = prefix ++ [new_number] ++ tail
  #       IO.inspect(new_list)

  #       if Advent2024.Day02A.check_change_list?(new_list) do
  #         true
  #       else
  #         is_dampened_safe?(tail, prefix ++ [head], head)
  #       end
  #   end
  # end

  def is_dampened_safe?(prefix, remain) do
    case remain do
      [] ->
        false

      [head | tail] ->
        new_list = prefix ++ tail |> Advent2024.Day02A.change_list()
        # IO.inspect(new_list)

        Advent2024.Day02A.check_change_list?(new_list) or is_dampened_safe?(prefix ++ [head], tail)
    end
  end

  def is_smart_safe?(measures) do
    # IO.inspect(measures)

    is_dampened_safe?([], measures)
  end

  def run(input) do
    input
    |> Enum.map(&is_smart_safe?/1)
    # |> IO.inspect()
    |> Enum.count(& &1)
  end
end
