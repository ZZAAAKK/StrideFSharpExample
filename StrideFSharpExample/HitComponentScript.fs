namespace GameExtension

open Stride.Engine
open Stride.Core.Mathematics
open Stride.Physics
open Stride.Input

type HitComponentScript(game : Game, entity : Entity) =
    inherit SyncScript()

    let mutable cameraComponent : CameraComponent option = None
    let mutable simulation : Simulation option = None

    override __.Start() = 
        cameraComponent <- game.SceneSystem.SceneInstance.RootScene.Entities 
            |> Seq.map (fun x -> x.Get<CameraComponent>())
            |> Seq.head
            |> Some
        simulation <- game.SceneSystem.SceneInstance.GetProcessor<PhysicsProcessor>().Simulation |> Some

    override __.Update() = 
        if Vector3.Distance(Vector3.Zero, entity.Transform.Position) > 30f then entity.Scene <- null

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
                | true -> 
                    let rb = hitResult.Collider.Entity.Get<RigidbodyComponent>()
                    if rb = null |> not then
                        rb.ClearForces()
                        rb.ApplyImpulse(new Vector3(-28f, 0f, -28f))
                | _ -> ()
        | _ -> ()