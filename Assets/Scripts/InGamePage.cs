using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class InGamePage : FContainer //FMultiTouchableInterface
{
	private FSprite _background;
	private PDPaddle _player1;
	private PDPaddle _player2;
	private PDBall _ball;

	public InGamePage ()
	{
		_background = new FSprite ("JungleBlurryBG");
		AddChild (_background);
		
		_player1 = new PDPaddle ("Player1");
		_player2 = new PDPaddle ("Player2");
		_ball = new PDBall ();
		ResetPaddles ();
		ResetBall ();
		AddChild (_player1);
		AddChild (_player2);
		AddChild (_ball);
		ListenForUpdate (Update);
	}

	private void ResetPaddles ()
	{
		_player1.x = -Futile.screen.halfWidth + _player1.width / 2;
		_player1.y = 0;
		
		_player2.x = Futile.screen.halfWidth - _player2.width / 2;
		_player2.y = 0;
	}

	private void ResetBall ()
	{
		_ball.x = 0;
		_ball.y = 0;
		// Ensure that the ball starts at a random angle that is never greater than 45 degrees from 0 in either direction
		_ball.yVelocity = (_ball.defaultVelocity / 2) - (RXRandom.Float () * _ball.defaultVelocity);
		// Make sure that the defaultVelocity (hypotenuse) is honored by setting the xVelocity accordingly, then choose a random horizontal direction
		_ball.xVelocity = Mathf.Sqrt ((_ball.defaultVelocity * _ball.defaultVelocity) - (_ball.yVelocity * _ball.yVelocity)) * (RXRandom.Int (2) * 2 - 1);
	}

	public void Update ()
	{
		float _newplayer1Y = _player1.y;
		float _newplayer2Y = _player2.y;
		float dt = Time.deltaTime;
		// Integrate to find the new x and y values for the ball
		float newBallX = _ball.x + dt * _ball.xVelocity;
		float newBallY = _ball.y + dt * _ball.yVelocity;
		
		// Handle Input
	    if (Input.GetKey("w")) { _newplayer1Y += dt * _player1.currentVelocity; }
	    if (Input.GetKey("s")) { _newplayer1Y -= dt * _player1.currentVelocity; }
	    if (Input.GetKey("up")) { _newplayer2Y += dt * _player2.currentVelocity; }
	    if (Input.GetKey("down")) { _newplayer2Y -= dt * _player2.currentVelocity; }

     
		// Render the ball at its new location
		_ball.x = newBallX;
		_ball.y = newBallY;
		_player1.y = _newplayer1Y;
	    _player2.y = _newplayer2Y;
	}
}

