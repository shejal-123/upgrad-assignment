from config import MOCK_API
from utils.decorators import log_call, retry

@log_call
@retry()
def create_ticket(incident):
    if MOCK_API:
        print(f"[MOCK SNOW] {incident.id}")
        return f"SNOW-{incident.id}"