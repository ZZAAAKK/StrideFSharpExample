namespace GameExtension

open Stride.Engine
open Stride.Core.Mathematics
open Stride.GameDefaults.Extensions
open Stride.GameDefaults.ProceduralModels

type GameExtension =
    static member AddBoxToScene (game : Game) =
        let entity = game.CreatePrimitive(PrimitiveModelType.Cube, material = game.NewDefaultMaterial(Color.DarkGoldenrod))
        entity.Transform.Position <- new Vector3(0f, 2.5f, 0f)
        entity.Scene <- game.SceneSystem.SceneInstance.RootScene
        entity