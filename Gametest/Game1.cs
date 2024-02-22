using OpenTK.Graphics.OpenGL;
using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Gametest.Template;
using OpenTK.Mathematics;


// using var game = new MFC.Game1();
//game.Run();
//hier eigentlich alles von programm rein und programm nur hier ausführen lassen
//die beiden 2zeiler drüber
/*
 * List<DrawInfo> objects = new List<DrawInfo>();
 * List<Renderable> renderobjects = new List<Renderable>();
 */

namespace Gametest
{

	//ErrorChecker.InitializeGLDebugCallback(); ?
	public class Game1 : GameWindow
	{
		Gamestate gameState = Gamestate.startmenu;
		View Main = new View();
		List<SubView> subViews = new List<SubView>();
		private int Width;
		private int Height;
		public List<DrawInfo> objects = new List<DrawInfo>();
		public float rotation = 0;

		public Game1(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() { Size = (800, 900), Title = "hi", Profile = ContextProfile.Compatability })
		{
			Width = width;
			Height = height;

		
			


			

			
			
			 Texture subViewsurface = new Texture(100, 100);
			 SubView subView = new SubView(new VBO(subViewsurface));
			
			 subView.addObject(new ColoredRectangle(new Vector2(50, 50), new Vector2(50, 50), Color4.Red));
			 subView.addObject(new ColoredRectangle(new Vector2(50, 50), new Vector2(10, 10), Color4.Blue));
			 
			 subViews.Add(subView);
			 TexturedRectangle t = new TexturedRectangle(subViewsurface);
				 t.DrawInfo.Position = new Vector2(0, 0);
			 Main.addObject(t);
			// Main.addObject(new ColoredRectangle(new Vector2(50, 50), new Vector2(100, 100), Color4.Red));
		
			//Main.addObject(dontdie);

			this.Resize += e => Main.Resize(e.Width, e.Height);

			//game.RenderFrame += _ => Main.draw();    // zu onrenderframe, override
			//game.RenderFrame += _ => game.SwapBuffers();    // zu onrenderframe, override

			this.KeyDown += e => Update(e);
			this.UpdateFrame += _ => rotation++;
			this.UpdateFrame += _ => urotatio();


		}



		//void update();

		void Update(KeyboardKeyEventArgs e)
		{
			//update gamestate
			/*
			switch(this.gameState)
            {
				case Gamestate.startmenu:
					//draw menu
					//this.backgroundroundColor = Color.Aqua;
					Console.WriteLine("Game starting");
					break;
				case Gamestate.settings:
					//draw settings
					Console.WriteLine("Settings");
					// this.Scene = new List<Drawable>() { this.backButton };
					break;
				case Gamestate.gameisrunning:
					//draw map und rest
					Console.WriteLine("Game running");
                    break;
				case Gamestate.gameover:
					//draw game over
					Console.WriteLine("Game stopped running, back to Menu");
					break;

            }
			*/


			switch (e.Key)
			{
				case Keys.W: Main.vpossition.Y += 10;
					subViews[0].vpossition.Y -= 10; break;
				case Keys.A: Main.vpossition.X -= 10; break;
				case Keys.S: Main.vpossition.Y -= 10; 
					subViews[0].vpossition.Y += 10; break;break;
				case Keys.D: Main.vpossition.X += 10; break;
				
				


				case Keys.E: Main.rotation++; break;
				case Keys.R: Main.vsize.X += 10; break;
				case Keys.F: Main.vsize.X -= 10; break;
				case Keys.Q: Main.rotation--; break;



			}
			Console.WriteLine("poss: " + Main.vpossition + "  size: " + Main.vsize + " rot: " + Main.rotation);
			
		}

		protected override void OnRenderFrame(FrameEventArgs args)
		{
			base.OnRenderFrame(args);

			base.OnRenderFrame(args);

			//this.RenderFrame += _ => Main.draw();    // zu onrenderframe, override
			foreach (var view in subViews)
			{
				view.draw();
			}
			
			Main.draw(); 
			//this.RenderFrame += _ => game.SwapBuffers();    // zu onrenderframe, override
			//this.RenderFrame += _ => this.SwapBuffers();
			this.SwapBuffers();


		}

		//irgendwann camera klasse machen, aber zuerst code aufräumen

		//this.backButton.SetCallback(BackHandler);
		//dann kann man nämlich sagen der backbutton hat macht immer das was im backhandler steht
		//wenn er gedrückt wird zb
		//public void BackHandler(Button self)
		// { this.gameState = GameState.StartMenu;  
		// }
        

	
	void urotatio()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			objects[i].Rotation = rotation;
		}
	}



	



	}


}

