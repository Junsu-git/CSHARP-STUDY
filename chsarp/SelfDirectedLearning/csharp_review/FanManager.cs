namespace reviewLib
{
    internal class FanManager
    {
        List<Fan> list = new List<Fan>();
        int Index { get; set; }

        public FanManager() { Index = 0; }

        public int GetListLength => list.Count;

        public void CreateFan() => list.Add(new Fan(Index++));

        public Fan GetFan(int index) => list[index];

        // 빈 리스트인가?
        public bool IsListEmpty()
        {
            return list.Count == 0;
        }

        // 입력한 값이 리스트의 길이보다 큰가?
        public bool IsListOverflow(int uInput)
        {
            return list.Count <= uInput;
        }

        public bool IsValidIndex(int uInput)
        {
            if (uInput >= 0 && !(IsListOverflow(uInput))) return true;
            else return false;
        } 
    }
}

/* 상태 변경 로직
 * 1. 사용자가 선풍기의 상태 변경 선택 
 * 2. 단일 선풍기 변경 / 전체 선풍기 변경 클릭 1. 단일 2. 전체 0. 뒤로가기
 * 2.1 단일 경우 몇 번의 선풍기인지 선택
 * 3. 선풍기 옵션 변경 메뉴 선택 1, 전원 2. 풍속 3. 회전 0. 뒤로가기
 * 3-1 전원 변경 입력 : 1. 켜기 2. 끄기 0. 뒤로가기
 * 3-2 속도 변경 입력 : 1. 미풍 2. 약풍 3. 강풍 0. 뒤로가기
 * 3-3 회전 변경 입력 : 1. 회전 2. 정지 0. 뒤로가기
 * 
 */