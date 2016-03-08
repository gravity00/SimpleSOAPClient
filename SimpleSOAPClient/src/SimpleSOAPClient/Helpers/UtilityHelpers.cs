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