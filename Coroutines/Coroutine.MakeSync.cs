using System;
using System.Collections.Generic;

namespace Coroutines {
    partial class Coroutine {
        public static Func<TArg1, TRet> MakeSynchronous<TArg1, TRet>(Func<TArg1, IEnumerable<TRet>> f) {
            Func<TArg1, TRet> wrapped = (v1) => {
                var co = new Coroutine<TRet>(f(v1)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TRet> MakeSyncWithArgs<TArg1, TRet>(Func<TArg1, IEnumerable<TRet>> f) {
            Func<object, TArg1, TRet> wrapped = (args, v1) => {
                var co = new Coroutine<TRet>(f(v1)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TRet> MakeSynchronous<TArg1, TArg2, TRet>(Func<TArg1, TArg2, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TRet> wrapped = (v1, v2) => {
                var co = new Coroutine<TRet>(f(v1, v2)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TRet> MakeSyncWithArgs<TArg1, TArg2, TRet>(Func<TArg1, TArg2, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TRet> wrapped = (args, v1, v2) => {
                var co = new Coroutine<TRet>(f(v1, v2)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TRet>(Func<TArg1, TArg2, TArg3, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TRet> wrapped = (v1, v2, v3) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TRet>(Func<TArg1, TArg2, TArg3, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TRet> wrapped = (args, v1, v2, v3) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TRet>(Func<TArg1, TArg2, TArg3, TArg4, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TRet> wrapped = (v1, v2, v3, v4) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TRet>(Func<TArg1, TArg2, TArg3, TArg4, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TRet> wrapped = (args, v1, v2, v3, v4) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TRet> wrapped = (v1, v2, v3, v4, v5) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TRet> wrapped = (args, v1, v2, v3, v4, v5) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet> wrapped = (v1, v2, v3, v4, v5, v6) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
#if !CO_FRANCA
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet> MakeSyncWithArgs<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, IEnumerable<TRet>> f) {
            Func<object, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TRet> wrapped = (args, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15)) { ThrowErrors = true };
                while (co.Resume(args).Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
        public static Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TRet> MakeSynchronous<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TRet>(Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, IEnumerable<TRet>> f) {
            Func<TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7, TArg8, TArg9, TArg10, TArg11, TArg12, TArg13, TArg14, TArg15, TArg16, TRet> wrapped = (v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16) => {
                var co = new Coroutine<TRet>(f(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16)) { ThrowErrors = true };
                while (co.Resume().Status != CoStatus.Dead) { }
                return co.Result.ReturnValue;
            };
            return wrapped;
        }
#endif
    }
}
