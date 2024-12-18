defmodule Advent2024.Day05B do
  alias Advent2024.Day05A
  # defp check_pairs([], _), do: true
  # defp check_pairs([_], _), do: true

  # defp check_pairs([head | tail], rules) do
  #   Enum.all?(tail, fn x -> valid_order?({head, x}, rules) end) and check_pairs(tail, rules)
  # end

  # defp valid_order?({early, late}, rules) do
  #   Enum.all?(rules, fn {a, b} ->
  #     !(early == b and late == a)
  #   end)
  # end
  defp sort_guide(guide, rules) do
    Enum.sort(guide, fn a, b ->
      Enum.any?(rules, fn {x, y} -> x == a and y == b end)
    end)
  end

  def run(input) do
    {rules, guides} = Day05A.parse_data(input)

    # IO.inspect(rules)
    # IO.inspect(guides)

    Enum.filter(guides, fn guide -> !Day05A.check_pairs(guide, rules) end)
    |> Enum.map(fn guide -> sort_guide(guide, rules) end)
    |> Enum.map(fn guide -> Enum.at(guide, div(length(guide), 2)) end)
    |> Enum.sum()
  end
end
