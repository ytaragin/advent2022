defmodule Advent2024.Day04A do
  @directions [
    # Right
    {0, 1},
    # Down
    {1, 0},
    # Left
    {0, -1},
    # Up
    {-1, 0},
    # Down-Right
    {1, 1},
    # Down-Left
    {1, -1},
    # Up-Right
    {-1, 1},
    # Up-Left
    {-1, -1}
  ]

  def create_matrix(strings) do
    Enum.map(strings, &String.graphemes/1)
  end

  def find_word(matrix, word) do
    for row <- 0..(length(matrix) - 1),
        col <- 0..(length(hd(matrix)) - 1),
        direction <- @directions,
        do: check_word(matrix, word, row, col, direction)
  end

  defp check_word(matrix, word, row, col, {dr, dc}) do
    check_word_recursive(matrix, String.graphemes(word), row, col, dr, dc)
  end

  defp check_word_recursive(_matrix, [], _row, _col, _dr, _dc), do: true

  defp check_word_recursive(matrix, [head | tail], row, col, dr, dc) do
    # IO.puts("Checking: #{head} - #{row}, #{col} - #{dr}, #{dc}")
    # new_row = row + dr
    # new_col = col + dc

    if is_same_letter?(matrix, row, col, head) do
      check_word_recursive(matrix, tail, row + dr, col + dc, dr, dc)
    else
      false
    end
  end

  def get_letter(matrix, row, col) do
    if valid_position?(matrix, row, col) do
      Enum.at(Enum.at(matrix, row), col)
    else
      nil
    end
  end

  def is_same_letter?(matrix, row, col, letter) do
    get_letter(matrix, row, col) == letter
  end

  defp valid_position?(matrix, row, col) do
    row >= 0 and row < length(matrix) and col >= 0 and col < length(hd(matrix))
  end

  def run(input) do
    matrix = create_matrix(input)

    find_word(matrix, "XMAS")
    # |> IO.inspect()
    |> Enum.count(&(&1 == true))
  end
end
