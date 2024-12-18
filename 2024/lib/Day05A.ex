defmodule Advent2024.Day05A do
  def split_sections(input) do
    {before_blank, after_blank} = Enum.split_while(input, fn line -> line != "" end)
    {before_blank, tl(after_blank)}
  end

  def parse_data(input) do
    {rules, guides} = split_sections(input)

    rules =
      Enum.map(rules, fn line ->
        line
        |> String.split("|")
        |> Enum.map(&String.to_integer/1)
        |> List.to_tuple()
      end)

    guides =
      Enum.map(guides, fn line ->
        line
        |> String.split(",")
        |> Enum.map(&String.to_integer/1)
      end)

    {rules, guides}
  end

  def check_pairs([], _), do: true
  def check_pairs([_], _), do: true

  def check_pairs([head | tail], rules) do
    Enum.all?(tail, fn x -> valid_order?({head, x}, rules) end) and check_pairs(tail, rules)
  end

  def valid_order?({early, late}, rules) do
    Enum.all?(rules, fn {a, b} ->
      !(early == b and late == a)
    end)
  end

  def run(input) do
    {rules, guides} = parse_data(input)

    # IO.inspect(rules)
    # IO.inspect(guides)

    Enum.filter(guides, fn guide -> check_pairs(guide, rules) end)
    |> Enum.map(fn guide -> Enum.at(guide, div(length(guide), 2)) end)
    |> Enum.sum()
  end
end
