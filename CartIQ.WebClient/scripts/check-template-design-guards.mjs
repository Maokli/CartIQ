import { readdir, readFile } from 'node:fs/promises';
import path from 'node:path';

const workspaceRoot = process.cwd();
const srcRoot = path.join(workspaceRoot, 'src');

const htmlHexPattern = /#[0-9a-fA-F]{3,8}\b/;
const arbitraryVisualClassPattern = /(text|bg|border|rounded|w|h|tracking)-\[[^\]]+\]/;
const nonSemanticTailwindColorPattern =
  /\b(?:bg|text|border|ring|fill|stroke|from|via|to|outline|decoration|caret|shadow)-(?:red|orange|amber|yellow|lime|green|emerald|teal|cyan|sky|blue|indigo|violet|purple|fuchsia|pink|rose|slate|gray|zinc|neutral|stone)-(?:50|100|200|300|400|500|600|700|800|900|950)\b/;

async function listHtmlFiles(directoryPath) {
  const entries = await readdir(directoryPath, { withFileTypes: true });
  const nested = await Promise.all(
    entries.map(async (entry) => {
      const fullPath = path.join(directoryPath, entry.name);

      if (entry.isDirectory()) {
        return listHtmlFiles(fullPath);
      }

      if (entry.isFile() && fullPath.endsWith('.html')) {
        return [fullPath];
      }

      return [];
    })
  );

  return nested.flat();
}

function findViolationsInContent(filePath, content) {
  const violations = [];
  const lines = content.split('\n');

  lines.forEach((line, index) => {
    if (htmlHexPattern.test(line)) {
      violations.push({
        filePath,
        line: index + 1,
        reason: 'Raw hex color found in template'
      });
    }

    if (arbitraryVisualClassPattern.test(line)) {
      violations.push({
        filePath,
        line: index + 1,
        reason: 'Arbitrary visual utility class found in template'
      });
    }

    if (nonSemanticTailwindColorPattern.test(line)) {
      violations.push({
        filePath,
        line: index + 1,
        reason: 'Non-semantic Tailwind palette color found in template - use a semantic token (bg-primary, text-muted-foreground, etc.) instead'
      });
    }
  });

  return violations;
}

async function run() {
  const htmlFiles = await listHtmlFiles(srcRoot);
  const allViolations = [];

  for (const htmlFile of htmlFiles) {
    const content = await readFile(htmlFile, 'utf8');
    const violations = findViolationsInContent(htmlFile, content);
    allViolations.push(...violations);
  }

  if (allViolations.length === 0) {
    console.log('Template design guards passed.');
    return;
  }

  console.error('Template design guard violations found:');

  allViolations.forEach((violation) => {
    const relativePath = path.relative(workspaceRoot, violation.filePath);
    console.error(`- ${relativePath}:${violation.line} ${violation.reason}`);
  });

  process.exitCode = 1;
}

run().catch((error) => {
  console.error(error);
  process.exitCode = 1;
});
