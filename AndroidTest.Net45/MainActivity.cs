using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidTest.Net45
{
    /// <summary>
    /// メインアクティビティを定義します。
    /// </summary>
    [Activity(Label = "LocusCommon Test", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // 非公開フィールド

        // 非公開フィールド :: コントロール
        private Button streamsTestButton;


        // コンストラクタ

        /// <summary>
        /// 
        /// </summary>
        public MainActivity()
        {

        }

        
        // 非公開メソッド

        /// <summary>
        /// View 上に配置されているコントロールのインスタンスを取得します。
        /// </summary>
        private void _getViewControls()
        {
            this.streamsTestButton = this.FindViewById<Button>(Resource.Id.streamsTestButton);
        }

        /// <summary>
        /// View 上に配置されているコントロールへイベントを追加します。
        /// </summary>
        private void _addEventsToControls()
        {
            this.streamsTestButton.Click += _testButtonsClickEventProc;
        }

        /// <summary>
        /// View 上に配置されているボタンの既定の処理を定義します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _testButtonsClickEventProc(object sender, System.EventArgs e)
        {
            if (sender == this.streamsTestButton)
            {
                // Streamのテストの開始
            }
        }


        // 限定公開メソッド

        /// <summary>
        /// アクティビティが初期化された直後に実行されます。
        /// </summary>
        /// <param name="savedInstanceState"></param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // アクティビティの共通初期処理
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.Main);

            // コントロールのインスタンスの取得
            this._getViewControls();

            // イベントの初期化
            this._addEventsToControls();
        }
    }
}

