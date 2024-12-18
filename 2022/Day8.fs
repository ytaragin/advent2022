module AdventDay8

let loadLines (filename: string) : string List =
    Seq.toList (System.IO.File.ReadLines(filename))

let toString: char seq -> string = Seq.map string >> String.concat ""





let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    // let lines = loadLines "input8A.txt"
    let lines = loadLines "input8short.txt"

    printfn "Lines: %d" lines.Length
