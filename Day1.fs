module AdventDay1 
    let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

    let splitGroup (groups: int list list) (line: string) : int list list =
        match line with
        | "" -> [] :: groups
        | _ -> (int (line) :: groups.Head) :: groups.Tail

    let sumItems (nums: seq<int>) : int = Seq.fold (+) 0 nums

    let createAndSortGroups (filename: string) : seq<int> =
        let lines: seq<string> = loadLines filename in

        Seq.fold splitGroup [ [] ] lines |> Seq.map sumItems |> Seq.sortDescending

    let firstNSum (n: int) (groups: seq<int>) : int = Seq.take n groups |> sumItems

    let run  =
        let ordered = createAndSortGroups "input1A.txt" in
        let day1 = firstNSum 1 ordered in
        let day1a = firstNSum 3 ordered in
            printfn "Top: %d Top 3: %d" day1 day1a
