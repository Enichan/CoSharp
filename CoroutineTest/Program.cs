using Coroutines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoroutineTest {
    class Program {
        static bool timedOut;

        static void Main() {
            var random = new Random();

            using (var co = Coroutine.Create(CoFunction1(5, 10))) {
                while (co.Resume()) {
                    if (co.Status != CoStatus.Waiting) {
                        Console.WriteLine(co.Result.ReturnValue.ToString());
                    }
                }
            }

            using (var co = Coroutine.Create(CoFunction2(10, 100))) {
                var i = 0;

                while (co.Resume(new { x = random.Next(10), y = random.Next(10) })) {
                    if (co.Status != CoStatus.Waiting) {
                        Console.WriteLine(co.Result.ReturnValue.ToString());
                    }

                    i++;
                    if (i > 5000000 && !timedOut) {
                        timedOut = true;
                        Console.WriteLine("timed out");
                    }
                }
            }

            var wrapped = Coroutine.Wrap(CoFunction1(5, 10));
            CoResult<int> wrappedResult;
            while ((wrappedResult = wrapped()) == true) {
                if (wrappedResult.Status != CoStatus.Waiting) {
                    Console.WriteLine("Wrapped coroutine: " + wrappedResult.ReturnValue);
                }
            }

            var sync = Coroutine.MakeSyncWithArgs<int, int, int>(CoFunction2);
            Console.WriteLine("Synchronized coroutine result: " + sync(new { x = 10, y = 100 }, 8, 20));

            Console.WriteLine("Press any key to run coroutine pool test");
            Console.ReadKey(true);

            var pool = new CoroutinePool();
            var resets = new Dictionary<Coroutine, int>();

            for (int i = 0; i < 5; i++) {
                var co = Coroutine.Create(PoolFunc(random.Next()));
                pool.Add(co);
                resets[co] = 0;
            }

            var reset = true;
            pool.OnEnd += (c) => {
                if (reset) {
                    c.Reset();
                    resets[c]++;
                }
            };

            ConsoleKey key;
            do {
                while (!Console.KeyAvailable) {
                    Console.Clear();
                    Console.WriteLine("Press 'q' to quit, 'r' to disable resets");
                    Console.WriteLine();

                    pool.ResumeAll();

                    var index = 1;
                    foreach (var co in pool) {
                        Console.SetCursorPosition(0, 1 + index);
                        Console.WriteLine("Coroutine " + index + ": " + co.ReturnValue);
                        Console.SetCursorPosition(20, 1 + index);
                        Console.WriteLine("Resets: " + resets[co]);
                        index++;
                    }

                    System.Threading.Thread.Sleep(50);

                    if (pool.IsEmpty) {
                        System.Threading.Thread.Sleep(200);
                        break;
                    }
                }

                if (pool.IsEmpty) {
                    break;
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.R) {
                    reset = false;
                }
            } while (key != ConsoleKey.Q);
        }

        static IEnumerable<int> PoolFunc(int seed) {
            var random = new Random(seed);
            var total = 0;
            while (total < 1000) {
                total += random.Next(25);
                yield return total;
            }
        }

        static IEnumerable<int> CoFunction1(int x, int y) {
            yield return 1 * x;
            Coroutine.WaitFor(1.25);
            yield return 2 * x;
            yield return 3 * y;
            yield return 4 * y;
        }

        static IEnumerable<int> CoFunction2(int a, int b) {
            var args = Coroutine.Args(new { x = 0, y = 0 });
            Console.WriteLine("args = " + args);

            yield return 1 * args.x * a;
            Console.WriteLine("args = " + Coroutine.Args(ref args));

            Coroutine.WaitWhile(() => !timedOut);
            yield return 2 * args.x * a;
            Console.WriteLine("args = " + Coroutine.Args(ref args));

            yield return 3 * args.y * b;
            Console.WriteLine("args = " + Coroutine.Args(ref args));

            yield return 4 * args.y * b;
            Console.WriteLine("args = " + Coroutine.Args(ref args));
        }
    }
}
