namespace YT.ChargeRecords.Dtos
{
    /// <summary>
    /// 充值记录新增和编辑时用Dto
    /// </summary>

    public class CreateOrUpdateChargeRecordInput
    {
        /// <summary>
        /// 充值记录编辑Dto
        /// </summary>
        public ChargeRecordEditDto ChargeRecordEditDto { get; set; }

    }
}
