using System;

namespace UnityLifetimeComparisons
{
    record CheckupCertificate
    {
        public int Id { get; init; }
        public string LifetimeManagerName { get; init; } = "";

        /// <summary>
        /// 同一コンテナからインスタンスを取得した場合、同一インスタンスか？
        /// </summary>
        public bool IsEqualInstanceFromSameContainer { get; init; }

        /// <summary>
        /// 親と子コンテナからインスタンスを取得した場合、同一インスタンスか？
        /// </summary>
        public bool IsEqualInstanceFromParentChildContainer { get; init; }

        /// <summary>
        /// 別スレッドの同一コンテナからインスタンスを取得した場合、同一インスタンスか？
        /// </summary>
        public bool IsEqualInstanceFromSameContainerOnOtherThread { get; init; }

        /// <summary>
        /// コンテナのDispose時に登録インスタンスをDisposeしてくれるか？
        /// </summary>
        public bool IsDisposedRegisteredInstance { get; init; }

        /// <summary>
        /// RegisterType()で登録済みのLifetimeManagerを再度別Typeの登録で使用できるか？
        /// </summary>
        public bool CanReuseLifetimeManager { get; init; }
    }
}
