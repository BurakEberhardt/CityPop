using Zen.CodeGeneration;
using Zen.Core.PlayerLoop;
using Zen.Core.View;
using Zen.Shared.Attributes;

namespace CityPop.Character
{
    // [DataBinding(typeof(TestData))]
    public partial class TestView : View
        , TestData.IAddedListener
        , TestData.IRemovedListener
        , TestData.IPlayersListener
        // , TestData.ITestListener
        // , TestData.IColorListener
        , TestData.PlayersList.IAddedListener
        , TestData.PlayersList.IRemovedListener
        , TestData.PlayersList.IClearListener
    {
        void TestData.IAddedListener.OnAdded(TestData testData)
        {
            
        }

        void TestData.IRemovedListener.OnRemoved()
        {
        }

        // [UpdateOnInitialize]
        // void TestData.ITestListener.OnTest(int test)
        // {
        // }
        //
        // [UpdateOnInitialize]
        // void TestData.IColorListener.OnColor(Color32 color)
        // {
        // }
        
        private global::CityPop.Character.TestData _testData;
        public global::CityPop.Character.TestData TestData
        {
            get => _testData;
            set
            {
                if (_testData != null)
                {
                    _testData.PlayersListEvents.RemoveAddedListener(this);
                    _testData.PlayersListEvents.RemoveRemovedListener(this);
                    _testData.PlayersListEvents.RemoveClearListener(this);
                    _testData.AddPlayersListener(this);
                    _testData.AddPlayersListener(_testDataPlayersListener);
                    _testDataPlayersListener.RemoveDeferredListener();
                    (this as global::CityPop.Character.TestData.IRemovedListener).OnRemoved();
                }

                _testData = value;
                
                if (_testData != null)
                {
                    _testData.PlayersListEvents.AddAddedListener(this);
                    _testData.PlayersListEvents.AddRemovedListener(this);
                    _testData.PlayersListEvents.AddClearListener(this);
                    _testData.AddPlayersListener(this);
                    _testDataPlayersListener.AddDeferredListener(this, _testData);
                    (this as global::CityPop.Character.TestData.IAddedListener).OnAdded(_testData);
                }
            }
        }

        // TODO: Implement deferred listeners like belowork
        // TODO: Instead of using Awake, Start, Update, OnDestroyed etc. use Attributes to tag certain methods and write code generator to make use of Awake + all Awake methods
        // TODO: Also do this for OnPooled / Unpooled
        // TODO: Find a way to modify list and update it without having to set it again
        TestDataPlayersListener _testDataPlayersListener;
        struct TestDataPlayersListener : TestData.IPlayersListener, IUpdate
        {
            TestData.IPlayersListener _listener;
            TestData _data;
            bool _dirty;

            public void AddDeferredListener(TestData.IPlayersListener listener, TestData data)
            {
                _listener = listener;
                _data = data;
                _data.AddPlayersListener(this);
            }

            public void RemoveDeferredListener()
            {
                if (_dirty)
                {
                    PlayerLoop.RemoveListener(this);
                    _dirty = false;
                }
                
                _data.RemovePlayersListener(this);
            }

            void TestData.IPlayersListener.OnPlayers(ListData<int> players)
            {
                if (!_dirty)
                {
                    Zen.Core.PlayerLoop.PlayerLoop.AddListener(this);
                    _dirty = true;
                }
            }

            void IUpdate.OnUpdate()
            {
                Zen.Core.PlayerLoop.PlayerLoop.RemoveListener(this);
                _dirty = false;
                
                _listener.OnPlayers(_data.Players);
            }
        }

        void TestData.PlayersList.IAddedListener.OnAdded(int element, int index)
        {
        }

        void TestData.PlayersList.IRemovedListener.OnRemoved(int element, int index)
        {
        }

        void TestData.PlayersList.IClearListener.OnClear()
        {
        }

        [DeferredUpdate]
        void TestData.IPlayersListener.OnPlayers(ListData<int> players)
        {

        }
    }
}