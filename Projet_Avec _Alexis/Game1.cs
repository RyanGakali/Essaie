using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projet_Avec__Alexis
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        // Textures du vaisseau
        Texture2D vaisseauAvant;
        Texture2D vaisseauGauche;
        Texture2D vaisseauDroite;

        Texture2D vaisseau;                 // texture du vaisseau affichée
        Vector2 vaisseauPosition;         // position du vaisseau à l'écran

        Texture2D arrierePlan;              // texture d'arrière-plan (les étoiles)

        float vitesseLaterale = 0.0f;
        float vitesseFrontale = 0.0f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Permet au jeu d'effectuer toute initialisation avant de commencer à jouer.
        /// Cette fonction membre peut demander les services requis et charger tout contenu
        /// non graphique pertinent. L'invocation de base.Initialize() itèrera parmi les
        /// composants afin de les initialiser individuellement.
        /// </summary>
        protected override void Initialize()
        {
            // À FAIRE: Ajoutez votre logique d'initialisation ici.
            vaisseauPosition = new Vector2(100.0f, 100.0f);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent est invoquée une seule fois par partie et permet de
        /// charger tous vos composants.
        /// </summary>
        protected override void LoadContent()
        {
            // Créer un nouveau SpriteBatch, utilisée pour dessiner les textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Charger les textures du vaisseau
            vaisseauAvant = Content.Load<Texture2D>("shipSprite");
            vaisseauGauche = Content.Load<Texture2D>("shipLeft");
            vaisseauDroite = Content.Load<Texture2D>("shipRight");

            // Charger les textures d'arrière-plan
            arrierePlan = Content.Load<Texture2D>("spaceBackground");

            // Initialement, le vaisseau ne se déplace pas
            vaisseau = vaisseauAvant;
        }

        /// <summary>
        /// UnloadContent est invoquée une seule fois par partie et permet de
        /// libérer vos composants.
        /// </summary>
        protected override void UnloadContent()
        {
            // À FAIRE: Libérez tout contenu de ContentManager ici
        }

        /// <summary>
        /// Permet d'implanter les comportements logiques du jeu tels que
        /// la mise à jour du monde, la détection de collisions, la lecture d'entrées
        /// et les effets audio.
        /// </summary>
        /// <param name="gameTime">Fournie un instantané du temps de jeu.</param>
        protected override void Update(GameTime gameTime)
        {
            // Permettre de quitter le jeu via la manette ou le clavier.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Changer la texture du vaisseau selon sa direction de déplacement latéral
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && !Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                vaisseau = vaisseauGauche;
                vaisseauPosition.X = System.Math.Max(vaisseauPosition.X - gameTime.ElapsedGameTime.Milliseconds * 0.5f, 0.0f);  // déplacer vers la gauche
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && !Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                vaisseau = vaisseauDroite;
                vaisseauPosition.X = System.Math.Min(vaisseauPosition.X + gameTime.ElapsedGameTime.Milliseconds * 0.5f, _graphics.GraphicsDevice.Viewport.Width - vaisseau.Width);  // déplacer vers la droite
            }
            else
                vaisseau = vaisseauAvant;

            base.Update(gameTime);


            // Changer la position verticale selon la touche pressée.
            if (Keyboard.GetState().IsKeyDown (Keys.Up ) && !(Keyboard.GetState().IsKeyDown (Keys.Down)))
                    vitesseFrontale = -0.5f; // appliquer une vitesse en Y décrémentale
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && !(Keyboard.GetState().IsKeyDown(Keys.Up)))
                vitesseFrontale = 0.5f; //Appliquer une vitesse en Y inccrémentale
            else
                vitesseFrontale = 0f;


        }

        /// <summary>
        /// Cette fonction membre est invoquée lorsque le jeu doit mettre à jour son 
        /// affichage.
        /// </summary>
        /// <param name="gameTime">Fournie un instantané du temps de jeu.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // À FAIRE: Ajoutez votre code d'affichage ici.
            _spriteBatch.Begin();
            _spriteBatch.Draw(arrierePlan, Vector2.Zero, Color.White);
            _spriteBatch.Draw(vaisseau, vaisseauPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
