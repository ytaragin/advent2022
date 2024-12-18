# Advent2024

This is the 2024 Advent of Code
I decide to try out Elixir this year.
As with other such projects, I am just learning elixir so the code may not be the best or the most idiomatic

## To Run
The easiest is with mix

```shell
mix run scripts/main.exs
```

Alternatively, can build and run with elixir

```shell
mix compile
elixir --erl "-pa _build/dev/lib/advent2024/ebin" -e "Elixir.Advent2024.Main.run()"
```

## Installation


Documentation can be generated with [ExDoc](https://github.com/elixir-lang/ex_doc)
and published on [HexDocs](https://hexdocs.pm). Once published, the docs can
be found at <https://hexdocs.pm/advent2024>.

