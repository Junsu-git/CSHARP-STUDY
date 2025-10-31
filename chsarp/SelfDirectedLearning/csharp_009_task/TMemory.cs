using System.Numerics;

namespace csharp_009_task
{
    internal class TMemory
    {
        // 계산할 최대 숫자
        public UInt128 MaxNum { get; set; }
        // 최종 결과 저장
        public UInt128 Result { get; set; }
        // 각 쓰레드별 부분 합계 저장
        public UInt128[] PartitionSum { get; set; }
        // 한 쓰레드가 담당할 범위
        public UInt128 Range { get; set; }

    }
}