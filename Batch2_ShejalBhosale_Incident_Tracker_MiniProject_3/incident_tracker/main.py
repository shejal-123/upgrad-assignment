import json
from utils.classifier import detect_type
from models.incident import NetworkIncident, AppIncident, SecurityIncident
from models.report import ReportGenerator
from services.jira import create_ticket as jira
from services.servicenow import create_ticket as snow
from services.azure_boards import create_ticket as azure


def create_object(data):
    text = data["title"] + data["description"]
    t = detect_type(text)

    if t == "network":
        return NetworkIncident(data)
    elif t == "security":
        return SecurityIncident(data)
    return AppIncident(data)


def main():
    with open("data/incidents.json") as f:
        raw = json.load(f)

    incidents = []

    for d in raw:
        obj = create_object(d)
        obj.classify()

        obj.ticket_ids["jira"] = jira(obj)
        obj.ticket_ids["snow"] = snow(obj)
        obj.ticket_ids["azure"] = azure(obj)

        incidents.append(obj)

    incidents.sort()

    report = ReportGenerator()
    report.generate_html(incidents)
    report.export_json(incidents)

    print("DONE...")


if __name__ == "__main__":
    main()