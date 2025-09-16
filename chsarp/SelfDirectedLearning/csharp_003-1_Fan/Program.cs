namespace csharp_003_1_Fan
{
    internal class Program
    {       
        static void Main(string[] args)
        {
            //SmartFan smFan = new SmartFan();
            Fan fan = new Fan("FAN_1", 4000);

            Console.WriteLine("선풍기 초기 상태 출력");
            Console.WriteLine();
            fan.PrintFan();
            Console.WriteLine();

            fan.PowerOn();
            fan.controlSpeed(Fan.PWR_SPEED.SPD_LV_1);
            fan.controlRotate(Fan.PWR_SWING.SWING_ON);

            Console.WriteLine("변경된 선풍기 상태 출력");
            fan.PrintFan();

            Console.WriteLine();

            // 입력 받은 후에 온도 값 체크해서 이제 해보자 
            Console.ReadLine();

            // 새 선풍기 생성 (이름 or index, 타이머 = null, 현재 온도, 최소 온도(선풍기 꺼질), 최대 온도( 선풍기 다시 켜질))
            // * 생성자에서 다 해도 되고, set 함수를 통해 따로 초기화 해줘도 됨
            

            // 해당 선풍기의 전원 켠 뒤, 출력 (이름, 상태(회전 전원 속도), 현재 온도, 목표 온도, 출력 시간 등을 로그로 작성
            // 추후 파일 출력을 통해 로그화 할 예정

            // 선풍기 상태 변경 (이후 선택 사항)
            // 전원 상태 변경
            // 속도 변경
            // 회전 상태 변경

            // 다시 출력 해보기

            // 입력 대기를 통한 블로커

            // power 가 꺼졌다가 켜지는걸 반복 하는지 확인
            // 꺼질때는 속도, 회전을 꺼짐 상태로 바꾸고 출력
            // 다시 켜질떄는, 상태를 변경 했을때의 상태로 변경후 출력

        }
    }

   
}
