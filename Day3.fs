module AdventDay3

let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

let toString: char seq -> string = Seq.map string >> String.concat ""

let splitLine line =
    let s = String.length line / 2
    let first = line.[..s]
    let second = line.[s..]
    (first, second)

let uniquefy (l: seq<char>) =
    Seq.countBy (fun x -> x) l
    |> Seq.choose (fun p ->
        match p with
        | (c, v) when (v = 1) -> Some c
        | (_) -> None)



let findMatch2 (e1: string, e2: string) =
    let l1 = uniquefy e1
    let l2 = uniquefy e2

    Seq.append l1 l2
    |> Seq.countBy (fun x -> x)
    |> Seq.choose (fun p ->
        match p with
        | (c, v) when (v > 1) -> Some c
        | (_) -> None)
    |> Seq.head


let cont (s: string) (c: char) = s.Contains c


let getPrio (c: char) : int =
    if c >= 'a' && c <= 'z' then int c - int 'a' + 1
    elif c >= 'A' && c <= 'Z' then int c - int 'A' + 27
    else 0

let findMatch (entry) =
    let (e1, e2) = splitLine entry
    // printfn "S1= %s S2=%s" e1 e2
    let func = cont e2
    Seq.filter func e1 |> Seq.head |> getPrio


let findMatching (str1: seq<char>) (str2: string) =
    let func = cont str2
    Seq.filter func str1



let calcScores (containers: seq<string>) = Seq.map findMatch containers |> Seq.sum


let createContainers (filename: string) : seq<(string * string)> = loadLines filename |> Seq.map splitLine

let findCommon (groups: string array) =
    printfn "%A" groups
    let stage1 = findMatching groups[0] groups[1]

    printfn "%s" (toString stage1)
    let stage2 = findMatching stage1 groups[2]
    printfn "%s" (toString stage2)
    Seq.head stage2 |> getPrio


let makeGroups (lines: seq<string>) =
    for s in lines do
        printfn "s %s" s

    // Seq.windowed 3 lines
    // |> Seq.filter (fun a -> a.Length = 3)
    // |> Seq.map findCommon
    // |> Seq.sum
    let l = Seq.windowed 3 lines

    for x in l do
        printfn "x: %A" x

    // Seq.head l |> findCommon
    let y = Seq.map findCommon l

    for x in y do
        printfn "x: %d" x

    0





let rec calcGroup (lines: string List) (acc: int) =
    if Seq.isEmpty lines then
        acc
    else
        let a :: b :: c :: d = lines
        let s = findCommon [| a; b; c |]
        calcGroup d acc + s



let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    let containers: seq<string> = loadLines "input3A.txt"

    // let s1 = Seq.head containers
    // printfn "Trying %s" s1

    // let score1 = calcScores containers
    let score2 = calcGroup (Seq.toList containers) 0
    // let score = findMatch s1
    printfn "Day3A %d Day3B %d " 0 score2
