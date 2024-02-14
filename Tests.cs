public class Tests{

    [Test]
    public void SanityCheck(){
        Func<int, int> ff = x => x;
        var source = new[] { ff };
        var result = source.InteresectOn(source);
        Assert.That(result.Count(), Is.EqualsTo(1));
    }

    [Test]
    public void TestUno()
    {
        var source = new List<Func<int, bool>> {
            n => true; n=>n%2==0,n=>false
            };
        var other = new List<Func<int, bool>> {
            n => n%3==0; n=>n<10,n=>n%5==0
        };
        var result = source.InteresectOn(other, 6);
        Assert.That(result,Is.EqualTo(new[]{true,true,true}));
    }


    [Test]
    public void TestDue()
    {  
        const int size = 20;
        var sourceCalls = new int[size];
        var otherCalls = new int[size];
        var source = new Func<int,int>[size];
        var other = new Func<int,int>[size];
        for(int i=0; i<size; i++)
        {
            //Perchè sennò cattura la variabile alla fine del ciclo
            //quando vado a valutare l'istruzione
            //Quindi sarebbero tutti di dimensione size+1; 
            var my_i = i;
            source[i] = n=>{
                sourceCalls[my_i]++;
                return my_i + 1;
            };
            otherCalls[i] = n=>{
                otherCalls[my_i]++;
                return n*(my_i + 1);
            };
        }

        var result = source.IntersectOn(other, 42).Count();
        Assert.Multiple(() => {
            for(int i=0;i<size;i++)
            {
                Assert.That(sourceCalls[i],Is.EqualTo(1));
                Assert.That(otherCalls[i],Is.EqualTo(1));
            }
        })

    }

    [Test]
    public void TestDueMoq()
    {
        var mock_source = new List<Mock<Func<int,int>>>();
        for(int i =0; i<20; i++)
        {
            var moq_source = new Mock<Func<int,int>>();
            moq_source.Setup(x => x(i)).Returns(i+1);
            mock_source.Add(moq_source);
        }

        for(int i =0; i<20; i++)
        {
            var moq_other = new Mock<Func<int,int>>();
            moq_other.Setup(x => x(i)).Returns(i+1 *i);
            moq_other.Add(moq_other);
        }

        var result = mock_source.Select(item => item.Object).IntersectOn(
            (IEnumerable<Func<int,int>>)Mock_other,42);
        
        for int(int i=0; i<20; i++)
        {
            mock_source[i].Verify();
        }

    }

    [Test]
    public void TestTre()
    {
        IEnumerable<Func<string,char>> Other()
        {
            while(true){
                yield return s => 'a';
            }
        }

        var source = new Func<string,char>[]{ s=> 'b'};
        //MAI USARE ASSERT THROW!! DEPRECATO
        //TAKE NON VISITA, CREA UN ALTRO ENUMERABLE
        Assert.That(()=>{
            source.IntersectOn(Other(), "pippo").ToArray();
        }, Throws.TypeOf<ArgumentExceptio>());

    }

    [Test]
    public void TestQuattro()
    {
        var source = new List<Func<int, bool>> {
            n => true; n=>(n%2==0)?throw new ArgumentException():true,n=>n<7;
            };
        var other = new List<Func<int, bool>> {
            n => false; n=>n<10,n=>n%5==0
        };
        var result = source.InteresectOn(other, 666);
        Assert.That(result,Is.EqualTo(new Boolean?[]{false,null,true}));
    }

}