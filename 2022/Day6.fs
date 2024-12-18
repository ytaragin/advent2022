module AdventDay6

let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

let toString: char seq -> string = Seq.map string >> String.concat ""


let isAMarker (buffer: char[]) =
    (Set.ofArray buffer |> Set.count) = buffer.Length

let findMarker (size: int) (test: seq<char>) =
    Seq.windowed size test |> Seq.findIndex isAMarker |> (+) size


let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    let lines: string = System.IO.File.ReadAllText "input6A.txt"
    let c1 = findMarker 4 lines
    let c2 = findMarker 14 lines
    printfn "Part 1: %d Part 2: %d" c1 c2
