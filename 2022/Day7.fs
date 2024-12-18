module AdventDay7

let loadLines (filename: string) : string List =
    Seq.toList (System.IO.File.ReadLines(filename))

let toString: char seq -> string = Seq.map string >> String.concat ""




type Action =
    | CD = 0
    | UP = 1
    | LS = 2
    | AddDir = 3
    | AddFile = 4
    | Unknown = -1

let parseLine (line: string) =
    let (|Prefix|_|) (p: string) (s: string) =
        if s.StartsWith(p) then
            Some(s.Substring(p.Length))
        else
            None

    match line with
    | Prefix "$ cd .." (_) -> (Action.UP, "")
    | Prefix "$ ls" (_) -> (Action.LS, "")
    | Prefix "$ cd " (rest: string) -> (Action.CD, rest)
    | Prefix "dir " (rest: string) -> (Action.AddDir, rest)
    | _ -> (Action.AddFile, line)

type DirectoryInfo =
    { name: string
      mutable childDirs: DirectoryInfo list
      mutable childFiles: FileInfo list }

and FileInfo = { name: string; size: int }

and FileSystemItem =
    | DirectoryInfo of DirectoryInfo
    | FileInfo of FileInfo


let rec getSize (f: DirectoryInfo) : int =
    // let rec addUpChildren (children: DirectoryInfo list) : int =
    //     List.fold (fun (curr: int) (child: FileSystemItem) -> curr + getSize (child)) 0 children

    let filesSizes =
        List.fold (fun (curr: int) (child: FileInfo) -> curr + child.size) 0 f.childFiles

    let dirSizes =
        List.fold (fun (curr: int) (child: DirectoryInfo) -> curr + getSize (child)) 0 f.childDirs

    filesSizes + dirSizes


let rec getSizeWithFilter (f: DirectoryInfo) (size: int) : int * int =
    let filesSizes =
        List.fold (fun (curr: int) (child: FileInfo) -> curr + child.size) 0 f.childFiles

    let (dirSizes, filteredTotal) =
        f.childDirs
        |> List.fold
            (fun (status: int * int) (child: DirectoryInfo) ->
                let (insize, infilteredsize) = status
                let (childsize, childfilteredsize) = getSizeWithFilter child size
                (insize + childsize, infilteredsize + childfilteredsize))
            (0, 0)

    let thisSize = filesSizes + dirSizes

    let newTotal =
        if thisSize <= size then
            filteredTotal + thisSize
        else
            filteredTotal

    (thisSize, newTotal)

let rec getDirSizes (f: DirectoryInfo) (currList: int list) : int * int list =
    printfn "Function called on %s with %A" f.name currList

    let filesSizes =
        List.fold (fun (curr: int) (child: FileInfo) -> curr + child.size) 0 f.childFiles

    let (dirSizes: int, dir: int list) =
        f.childDirs
        |> List.fold
            (fun (status: int * int list) (child: DirectoryInfo) ->
                let (insize: int, inlist: int list) = status
                let (childsize: int, childlist) = getDirSizes child inlist
                (insize + childsize, childsize :: childlist))
            (0, currList)

    let thisSize = filesSizes + dirSizes

    printfn "Adding %d to %A" thisSize dir
    (thisSize, dir)



let getFolderToGetSize (f: DirectoryInfo) (totalSpace: int) (neededSpace: int) =
    let (totSize, dirList) = getDirSizes f []
    let remaingSpace = totalSpace - totSize
    let needToFree = neededSpace - remaingSpace
    printfn "Need to free %d" needToFree
    printfn "%A" (List.sort dirList)

    List.sort dirList |> List.find ((<=) needToFree)


let getChild (dir: DirectoryInfo) (childName: string) : DirectoryInfo =
    // printfn "Searching for %s in %O" childName dir
    dir.childDirs |> List.find (fun (f) -> f.name = childName)

let addItem (dir: DirectoryInfo) (command: string) =
    let (a, label) = parseLine command

    match a with
    // | Action.AddDir -> dir.childDirs.Add(DirectoryInfo(label, new List<DirectoryInfo>(), new List<FileInfo>()))
    | Action.AddDir ->
        dir.childDirs <-
            { name = label
              childDirs = []
              childFiles = [] }
            :: dir.childDirs
    | Action.AddFile ->
        let opts = label.Split ' '
        // dir.childFiles.Add(FileInfo(opts[0], int opts[1]))
        dir.childFiles <- { name = opts[1]; size = (int opts[0]) } :: dir.childFiles


let rec handleLSLines (currDir: DirectoryInfo) (commands: string List) : string List =
    match commands with
    | [] -> []
    | (cmdStr: string) :: (tail: string list) ->
        if cmdStr.StartsWith("$") then
            commands
        else
            addItem currDir cmdStr // side effect here
            handleLSLines currDir tail

let rec procCommands (currDir: DirectoryInfo) (commands: string List) : string List =
    match commands with
    | [] -> []
    | (cmdStr: string) :: (tail: string list) ->
        let (cmd, str) = parseLine cmdStr

        let (cont, remainder) =
            match cmd with
            | Action.CD ->
                let child = getChild currDir str
                (true, procCommands child tail)
            | Action.UP -> (false, tail)
            | Action.LS -> (true, handleLSLines currDir tail)
            | Action.Unknown -> (true, tail)

        if cont then procCommands currDir remainder else remainder







let run: unit =
    // let containers: seq<string> = loadLines "test.txt"
    let lines = loadLines "input7A.txt"
    // let lines = loadLines "input7short.txt"
    // let root = DirectoryInfo("/", new List<DirectoryInfo>(), new List<FileInfo>())
    let root =
        { name = "/"
          childDirs = []
          childFiles = [] }

    procCommands root lines


    // let root: FileSystemItem =
    //     DirectoryInfo(
    //         "/",
    //         [ FileInfo("f1", 123)
    //           FileInfo("f2", 123)
    //           DirectoryInfo("/", [ FileInfo("f1", 200); FileInfo("f2", 300) ]) ]
    //     )

    // let root =
    //     DirectoryInfo
    //         { name = "/"
    //           subitems = [ FileInfo { name = "name"; size = 45 } ] }

    // printfn "%O" root

    let s = getSize root
    printfn "%d" s

    let (tot, filt) = getSizeWithFilter root 100000
    printfn "Total Size %d Filtered %d" tot filt

    let m = getFolderToGetSize root 70000000 30000000
    printfn "Min to free %d" m
