namespace OrderTask.Common.Enum
{
    public enum EnumOrderLogType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add = 1,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 2,
        /// <summary>
        ///接单
        /// </summary>
        Receive = 3,
        /// <summary>
        /// 拒绝
        /// </summary>
        Refuse = 4,
        /// <summary>
        /// 确认完成
        /// </summary>
        Confirm = 5,
        /// <summary>
        /// 取消
        /// </summary>
        Close=6,
        /// <summary>
        /// 评价
        /// </summary>
        Evaluate=7,
        /// <summary>
        /// 重新指派
        /// </summary>
        ReAppont=8,

        /// <summary>
        /// 变更订单状态
        /// </summary>
        ChangeOrderstate = 8
    }
}