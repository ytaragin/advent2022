module AdventDay2

let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

let Rock = 1
let Paper = 2
let Scissor = 3

let Win = 6
let Tie = 3
let Loss = 0



let splitToPair (line: string) =
    match line.Split ' ' with
    | [| a; b |] -> a, b
    | _ -> failwith "size must be 2"



let genScorePartA pair =
    match pair with
    | ("A", "X") -> Rock + Tie
    | ("A", "Y") -> Paper + Win
    | ("A", "Z") -> Scissor + Loss
    | ("B", "X") -> Rock + Loss
    | ("B", "Y") -> Paper + Tie
    | ("B", "Z") -> Scissor + Win
    | ("C", "X") -> Rock + Win
    | ("C", "Y") -> Paper + Loss
    | ("C", "Z") -> Scissor + Tie
    | (_, _) -> 0

let genScorePartB pair =
    match pair with
    | ("A", "X") -> Scissor + Loss
    | ("A", "Y") -> Rock + Tie
    | ("A", "Z") -> Paper + Win
    | ("B", "X") -> Rock + Loss
    | ("B", "Y") -> Paper + Tie
    | ("B", "Z") -> Scissor + Win
    | ("C", "X") -> Paper + Loss
    | ("C", "Y") -> Scissor + Tie
    | ("C", "Z") -> Rock + Win
    | (_, _) -> 0


let calcScores pairs calc =
    Seq.fold (fun (tot: int) (curr) -> tot + calc curr) 0 pairs

let run: unit =
    // let pairs : seq<seq<string>> =
    let pairs = loadLines "input2A.txt" |> Seq.map splitToPair
    let partA = calcScores pairs genScorePartA
    let partB = calcScores pairs genScorePartB

    printfn "PartA %d PartB %d" partA partB
