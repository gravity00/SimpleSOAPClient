#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimpleSOAPClient.Helpers
{
#if NET40
    using System;
    using System.Threading.Tasks;

    internal static class UtilityHelpers
    {
        public static Exception NewDefaultException()
        {
            return new Exception("The task was in faulted state, but no exception was provided.");
        }

        public static void SetExceptionFromTask<T>(this TaskCompletionSource<T> tcs, Task task)
        {
            if (tcs == null) throw new ArgumentNullException(nameof(tcs));
            if (task == null) throw new ArgumentNullException(nameof(task));

            if (!task.IsFaulted) return;

            if (task.Exception == null) //  It should never happen
                tcs.SetException(NewDefaultException());
            else
            {
                task.Exception.Flatten().Handle(ex =>
                {
                    tcs.SetException(ex ?? NewDefaultException());
                    return true;
                });
            }
        }

        public static T ThrowIfFaultedOrGetResult<T>(this Task<T> task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));

            if (!task.IsFaulted) return task.Result;
            if (task.Exception == null) //  It should never happen
                throw NewDefaultException();
            throw task.Exception.Flatten().InnerException;
        }

        public static void ThrowIfFaulted(this Task task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));

            if (!task.IsFaulted) return;
            if (task.Exception == null) //  It should never happen
                throw NewDefaultException();
            throw task.Exception.Flatten().InnerException;
        }
    }

#endif
}