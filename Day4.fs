module AdventDay4

let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

let toString: char seq -> string = Seq.map string >> String.concat ""

let splitLine (line: string) =
    let parts = line.Split ","

    let split2 (str: string) =
        match str.Split "-" with
        | [| a; b |] -> int a, int b
        | _ -> failwith "size must be 2"

    (split2 parts[0], split2 parts[1])


let isFullSubset (p1s: int, p1e: int) (p2s: int, p2e: int) = p1s >= p2s && p1e <= p2e

let isPartialSubset (p1s: int, p1e: int) (p2s: int, p2e: int) =
    (p1s >= p2s && p1s <= p2e) || (p1e >= p2s && p1s <= p2e)

let isOverlapCount subsetFunc (pair1, pair2) =
    if subsetFunc pair1 pair2 || subsetFunc pair2 pair1 then
        1
    else
        0

let countOverlaps subsetFunc (lines: seq<string>) =
    Seq.map splitLine lines |> Seq.map (isOverlapCount subsetFunc) |> Seq.sum

let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    let lines: seq<string> = loadLines "input4A.txt"
    let ca = countOverlaps isFullSubset lines
    let cb = countOverlaps isPartialSubset lines
    printfn "4a: %d 4b: %d" ca cb
