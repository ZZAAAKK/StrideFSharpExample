open Stride.Core.Mathematics
open Stride.Engine
open Stride.GameDefaults.ProceduralModels
open Stride.GameDefaults.Extensions

let game = new Game()

let Start rootScene = 
    game.SetupBase3DScene()
    let entity = game.CreatePrimitive(PrimitiveModelType.Capsule)
    entity.Transform.Position <- new Vector3(0f, 8f, 0f)
    entity.Scene <- rootScene

[<EntryPoint>]
let main argv =
    game.Run(start = Start)
    0