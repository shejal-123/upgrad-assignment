import functools
import time

def log_call(func):
    @functools.wraps(func)
    def wrapper(*args, **kwargs):
        print(f"[LOG] Calling {func.__name__}")
        result = func(*args, **kwargs)
        print(f"[LOG] Completed {func.__name__}")
        return result
    return wrapper


def retry(times=3, delay=1):
    def decorator(func):
        @functools.wraps(func)
        def wrapper(*args, **kwargs):
            for attempt in range(1, times + 1):
                try:
                    return func(*args, **kwargs)
                except Exception as e:
                    print(f"[Retry] Attempt {attempt} failed")
                    if attempt == times:
                        raise
                    time.sleep(delay)
        return wrapper
    return decorator