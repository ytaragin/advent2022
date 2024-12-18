# defmodule MyProject.Main do
#   def run do
#     parts = [
#       {Advent2024.Day01A, "inputs/day01_small.txt"},
#       {Advent2024.Day01A, "inputs/day01.txt"},
#       {Advent2024.Day01B, "inputs/day01_small.txt"},
#       # Add all parts here
#     ]

#     Enum.each(parts, fn {module, file} ->
#       input = File.read!(file)  |> to_string()
#       lines = String.split(input, "\n", trim: true)
#       result = module.run(lines)
#       IO.puts("Result for #{file}: #{inspect(result)}")
#     end)
#   end
# end

# MyProject.Main.run()
Advent2024.Main.run()
