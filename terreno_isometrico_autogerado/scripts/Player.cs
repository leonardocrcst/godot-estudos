using Godot;
using System.Collections.Generic;

public partial class Player : Area2D
{
	private Dictionary<string, string> teclasDirecao = new Dictionary<string, string>();
	private string direcao = "ToBottomRight";
	private Dictionary<string, int> posePersonagemParado = new Dictionary<string, int>();
	private bool personagemAndando = false;
	private string teclaPressionada = null;
	public int Velocidade {get; set;} = 100;
	public Vector2 TamanhoDaTela;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TamanhoDaTela = GetViewportRect().Size;
		DefinirTeclasDirecao();
		DefinirPosesPersonagem();
		
	}

	private void DefinirTeclasDirecao()
	{
		teclasDirecao["w"] = "ToUp";
		teclasDirecao["a"] = "ToLeft";
		teclasDirecao["d"] = "ToRight";
		teclasDirecao["s"] = "ToBottom";
	}

	private void DefinirPosesPersonagem()
	{
		posePersonagemParado["ToUpLeft"] = 5;
		posePersonagemParado["ToUp"] = 4;
		posePersonagemParado["ToUpRight"] = 3;
		posePersonagemParado["ToLeft"] = 6;
		posePersonagemParado["ToRight"] = 2;
		posePersonagemParado["ToBottomLeft"] = 7;
		posePersonagemParado["ToBottomRight"] = 1;
		posePersonagemParado["ToBottom"] = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AnimatedSprite2D animacao = GetNode<AnimatedSprite2D>("PlayerAnimation");
		Vector2 movimentacao = MoverPara();

		if (Input.IsActionJustPressed(teclaPressionada)) {

		}
		
		if (personagemAndando && movimentacao.Length() > 0) {
			if (!animacao.IsPlaying()) {
				VirarPara(direcao);
				animacao.Animation = direcao;
				animacao.Play();
			}
			movimentacao = movimentacao.Normalized() * Velocidade;
			Position += movimentacao * (float) delta;
		} else if (!personagemAndando && animacao.IsPlaying()) {
			animacao.Stop();
		}
		
	}

	private Vector2 MoverPara()
	{
		var movimentacao = Vector2.Zero;
		if (direcao.Contains("Bottom")) {
			movimentacao.Y += 1;
		}
		if (direcao.Contains("Up")) {
			movimentacao.Y -= 1;
		}
		if (direcao.Contains("Left")) {
			movimentacao.X -= 1;
		}
		if (direcao.Contains("Right")) {
			movimentacao.X += 1;
		}
		return movimentacao;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			string pressed = eventKey.KeyLabel.ToString().ToLower();
			if (eventKey.Pressed && teclasDirecao.ContainsKey(pressed)) {
				personagemAndando = true;
				teclaPressionada = pressed;
				direcao = teclasDirecao[pressed];
			} else {
				teclaPressionada = null;
				personagemAndando = false;
			}
		}
	}	

	private void VirarPara(string direcao)
	{
		AnimatedSprite2D animacao = GetNode<AnimatedSprite2D>("PlayerAnimation");
		animacao.Animation = "Idle";
		animacao.Frame = posePersonagemParado[direcao];
	}
}
