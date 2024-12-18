defmodule Advent2024.Day04B do
  alias Advent2024.Day04A

  defp up_left(), do: {-1, -1}
  defp up_right(), do: {-1, 1}
  defp down_left(), do: {1, -1}
  defp down_right(), do: {1, 1}

  def find_word(matrix, letters) do
    for row <- 0..(length(matrix) - 1),
        col <- 0..(length(hd(matrix)) - 1),
        do: check_spot(matrix, row, col, letters)
  end

  defp check_spot(matrix, row, col, letters) do
    Day04A.is_same_letter?(matrix, row, col, letters.letter) and
      check_sides(matrix, letters, row, col, up_left(), down_right()) and
      check_sides(matrix, letters, row, col, up_right(), down_left())
  end

  defp check_sides(matrix, letters, row, col, dir1, dir2) do
    letter1 = Day04A.get_letter(matrix, row + elem(dir1, 0), col + elem(dir1, 1))
    letter2 = Day04A.get_letter(matrix, row + elem(dir2, 0), col + elem(dir2, 1))

    (letter1 == letters.side1 and letter2 == letters.side2) or
      (letter1 == letters.side2 and letter2 == letters.side1)
  end

  def run(input) do
    matrix = Day04A.create_matrix(input)

    find_word(matrix, %{:letter => "A", :side1 => "M", :side2 => "S"})
    # |> IO.inspect()
    |> Enum.count(&(&1 == true))
  end
end
