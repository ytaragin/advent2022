module AdventDay5

open System.Text.RegularExpressions


let loadLines (filename: string) : seq<string> = System.IO.File.ReadLines(filename)

let toString: char seq -> string = Seq.map string >> String.concat ""


type Move = { amount: int; src: int; dest: int }

type Stacks = char list array

let splitLine (line) =
    let exp = "move (\d+) from (\d+) to (\d+)$"
    let r = new Regex(exp)
    let m = r.Match line

    let a :: s :: d :: _ =
        Seq.toList m.Groups |> List.tail |> List.map (fun x -> int x.Value)

    { amount = a
      src = s - 1
      dest = d - 1 }




let startingPos () : Stacks =
    [| [ 'T'; 'Z'; 'B' ]
       [ 'N'; 'D'; 'T'; 'H'; 'V' ]
       [ 'D'; 'M'; 'F'; 'B' ]
       [ 'L'; 'Q'; 'V'; 'W'; 'G'; 'J'; 'T' ]
       [ 'M'; 'Q'; 'F'; 'V'; 'P'; 'G'; 'D'; 'W' ]
       [ 'S'; 'F'; 'H'; 'G'; 'Q'; 'Z'; 'V' ]
       [ 'W'; 'C'; 'T'; 'L'; 'R'; 'N'; 'S'; 'Z' ]
       [ 'M'; 'R'; 'N'; 'J'; 'D'; 'W'; 'H'; 'Z' ]
       [ 'S'; 'D'; 'F'; 'L'; 'Q'; 'M' ] |]




let rec moveBoxes (boxes: Stacks) src dest amount =
    if amount = 0 || List.isEmpty boxes[src] then
        boxes
    else
        let s = List.head boxes[src]
        let sl = List.tail boxes[src]
        let d = boxes[dest]
        boxes.[dest] <- s :: d
        boxes.[src] <- sl
        moveBoxes boxes src dest (amount - 1)


let rec moveFromList (src: char list) dest amount =
    if amount = 0 || List.isEmpty src then
        (src, dest)
    else
        let s = List.head src
        let sl = List.tail src
        moveFromList sl (s :: dest) (amount - 1)



let moveBoxesB (boxes: Stacks) src dest amount =
    if amount = 0 || List.isEmpty boxes[src] then
        boxes
    else
        let (newSrc, temp) = moveFromList boxes[src] [] amount
        let (_, newDest) = moveFromList temp boxes[dest] amount
        boxes.[dest] <- newDest
        boxes.[src] <- newSrc
        boxes


let runMove moveFunc (boxes: Stacks) (move: Move) =
    moveFunc boxes move.src move.dest move.amount



let printTops (boxes: Stacks) =
    for stack in boxes do
        printf "%c" (List.head stack)

    printfn ""



let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    let lines: seq<string> = loadLines "input5A.txt"

    let movePart1 = runMove moveBoxes
    let movePart2 = runMove moveBoxesB
    let moves = Seq.map splitLine lines

    printf "Part 1: "
    Seq.fold movePart1 (startingPos ()) moves |> printTops

    printf "Part 2: "
    Seq.fold movePart2 (startingPos ()) moves |> printTops
