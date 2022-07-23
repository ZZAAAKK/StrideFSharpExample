open Stride.Engine
open Stride.GameDefaults.Extensions

open GameExtension

let game = new Game()

let Start rootScene = 
    game.SetupBase3DScene()
    game.AddProfiler() |> ignore
    let firstBox = GameExtension.AddBoxToScene game
    firstBox.Add(firstBox |> MoveComponentScript)
    let boxGenerator = new Entity()
    boxGenerator.Scene <- rootScene
    boxGenerator.Add((game, boxGenerator) |> BoxGeneratorScript)

[<EntryPoint>]
let main argv =
    game.Run(start = Start)
    0