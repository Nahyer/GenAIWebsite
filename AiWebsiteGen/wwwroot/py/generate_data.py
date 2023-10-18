import pandas as pd
import matplotlib.pyplot as plt
from io import BytesIO
import base64

# Load the CSV data
csv_file = '~/py/salaries2023.csv'
data = pd.read_csv(csv_file)

# Generate HTML table
table_html = data.to_html(classes='table table-bordered table-hover')

# Generate a bar chart
chart_data = data.groupby('job_title')['salary_in_usd'].mean()
plt.figure(figsize=(10, 5))
chart_data.plot(kind='bar')
plt.title('Average Salary by Job Title')
plt.xlabel('Job Title')
plt.ylabel('Average Salary (USD)')
plt.tight_layout()

# Save the chart as a BytesIO object and convert to base64
chart_buffer = BytesIO()
plt.savefig(chart_buffer, format='png')
chart_buffer.seek(0)
chart_base64 = base64.b64encode(chart_buffer.read()).decode('utf-8')

# Define a function to return HTML content
def generate_data_visualizations():
    return {
        'table_html': table_html,
        'chart_html': f'<img src="data:image/png;base64,{chart_base64}" alt="Average Salary Chart">'
    }

# Call the function to generate data visualizations
if __name__ == "__main__":
    result = generate_data_visualizations()
    print(result)
