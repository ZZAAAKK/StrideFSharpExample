namespace GameExtension

open Stride.Engine
open Stride.Core.Mathematics
open Stride.Physics
open Stride.Input

type BoxGeneratorScript(game : Game, entity : Entity) =
    inherit SyncScript()

    let mutable cameraComponent : CameraComponent option = None
    let mutable simulation : Simulation option = None

    override __.Start() =
        cameraComponent <- entity.Scene.Entities 
            |> Seq.where (fun x -> x.Get<CameraComponent>() = null |> not)
            |> Seq.head
            |> (fun e -> e.Get<CameraComponent>())
            |> Some
        simulation <- game.SceneSystem.SceneInstance.GetProcessor<PhysicsProcessor>().Simulation |> Some

    override __.Update() =
        match cameraComponent, simulation with
        | Some c, Some s -> 
            let input = game.Input
            if input.HasMouse && input.IsMouseButtonPressed(MouseButton.Left) then
                let inv = Matrix.Invert(c.ViewProjectionMatrix)
                let p = input.MousePosition
                let mousePosNear = new Vector3(p.X * 2f - 1f, 1f - p.Y * 2f, 0f)
                let mousePosFar = new Vector3(p.X * 2f - 1f, 1f - p.Y * 2f, 1f)
                let vectorNear = Vector3.Transform(mousePosNear, inv) |> (fun v -> v / v.W)
                let vectorFar = Vector3.Transform(mousePosFar, inv) |> (fun v -> v / v.W)
                let hitResult = s.Raycast(vectorNear.XYZ(), vectorFar.XYZ())
                
                match hitResult.Succeeded with
                | true -> ()
                | _ -> 
                    let e = GameExtension.AddBoxToScene game
                    e.Add((game, e) |> HitComponentScript)
        | _ -> ()