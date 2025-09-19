using Chap7;
using Chap7.MethodHiding;
using Chap7.Overriding;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1.RUN();
            //CopyActiver.Run();

            //ThisConstructor.Run(); // this 재할당
            //WaterHeater.Run(); // 캡슐화

            //InheritanceRun.Run(); // 상속

            //TypeCasting.Run(); // 상속과 형변환
            //Overrideing.Run(); // 다형성
            MethodHiding.Run(); // 메소드 숨김 처리(상속 받은 클래스 내부에 동일한 메소드가 존재하더라도 오버라이딩 하지 않기)


        }
    }
}
